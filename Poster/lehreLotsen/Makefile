#Makefile Lehre_Lotsen_2018 Latex Poster

docName=poster_hamburg
InstallDir=../../../out/Poster/Lehre_Lotsen_2018/${docName}/


all: install

build: ${docName}.tex
	 pdflatex -synctex=1 -interaction=nonstopmode --shell-escape "${docName}".tex

clean:
	rm -rf *.aux *.log *.nav *.out *.snm *.toc *.synctex.gz *.pdf

install: build
	mkdir -p  ${InstallDir}
	cp *.pdf ${InstallDir}
