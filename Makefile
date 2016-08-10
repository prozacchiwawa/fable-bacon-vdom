all: out out/vdomtest.js

clean:
	rm -rf out

out:
	mkdir $@

js/test.js: test.fsx
	fable --projFile $< --outDir js

out/vdomtest.js: js/test.js js/vdominterface.js
	browserify -e js/vdominterface.js -o $@

