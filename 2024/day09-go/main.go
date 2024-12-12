package main

import (
	"fmt"
	"io"
	"os"
	"strconv"
)

func main() {
	if err := do(); err != nil {
		fmt.Println(err)
		os.Exit(1)
	}
}

func do() error {

	file, err := os.Open("input")
	if err != nil {
		return err
	}
	defer file.Close()

	content, err := io.ReadAll(file)
	if err != nil {
		return err
	}

	files, err := parse(string(content))
	if err != nil {
		return err
	}

	err = moveFiles(files)
	if err != nil {
		return err
	}

	checkSum := calcCheckSum(files)

	println(checkSum)

	return nil
}

func calcCheckSum(files []File) int64 {
	checkSum := int64(0)
	i := int64(0)
	for _, f := range files {
		for k := int64(0); k < f.Space; k++ {
			fmt.Printf("%d += %d * %d\n", checkSum, i, f.Index)
			checkSum += f.Index * i
			i++
		}
		freeUsed := f.FreeSpace
		for _, f2 := range f.Moved {
			for k := int64(0); k < f2.Space; k++ {
				fmt.Printf("%d += %d * %d\n", checkSum, i, f2.Index)
				checkSum += f2.Index * i
				i++
				freeUsed++
			}
		}
		for ii := f.FreeSpace; ii > 0; ii-- {
			i++
		}
	}
	return checkSum
}

func moveFiles(files []File) error {
	for i := 0; i < len(files)-1; i++ {
		for j := len(files) - 1; j >= 0 && j > i; j-- {
			if files[i].DoesFit(files[j]) {

				if files[j].Index == 0 {
					continue
				}

				if err := files[i].MoveFile(files[j]); err != nil {
					return err
				}
				files[j].Index = 0
			}
		}

	}
	return nil
}

type File struct {
	Index     int64
	FreeSpace int64
	Space     int64
	Moved     []File
}

func (f File) DoesFit(m File) bool {
	return f.FreeSpace >= m.Space
}

func (f *File) MoveFile(m File) error {
	if f.FreeSpace < m.Space {
		return fmt.Errorf("can't mode file %d to %d", m.Index, f.Index)
	}
	f.FreeSpace -= m.Space
	f.Moved = append(f.Moved, m)
	return nil
}

func parse(content string) ([]File, error) {
	var result []File
	for i := 0; i < len(content)-1; i++ {
		a, err := strconv.ParseInt(string(content[i]), 10, 64)
		if err != nil {
			return nil, err
		}
		i++
		b, err := strconv.ParseInt(string(content[i]), 10, 64)
		if err != nil {
			return nil, err
		}
		result = append(result, File{
			Index:     int64(i) / 2,
			FreeSpace: b,
			Space:     a,
			Moved:     nil,
		})
	}
	a, err := strconv.ParseInt(string(content[len(content)-1]), 10, 64)
	if err != nil {
		return nil, err
	}

	result = append(result, File{
		Index:     int64(len(content)-1) / 2,
		FreeSpace: 0,
		Space:     a,
		Moved:     []File{},
	})

	return result, nil
}
