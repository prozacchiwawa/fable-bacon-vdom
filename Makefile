all: out out/vdomtest.js

clean:
	rm -rf out

out:
	mkdir $@

out/test.js: test.fsx
	fable $<

out/vdomtest.js: out/test.js js/vdominterface.js
	browserify -e js/vdominterface.js -o $@

