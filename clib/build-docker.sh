#!/bin/sh

cd /build/src/libsmb2

echo "Running git clean..."
git clean -x -d -f > /dev/null 2>&1

echo "Running bootstrap..."
./bootstrap > /dev/null 2>&1

echo "Configuring..."
./configure --without-libkrb5 --disable-dependency-tracking > /dev/null 2>&1
echo "Building..."
make > /dev/null 2>&1

mkdir -p ../../bin/linux-x64/
cp lib/.libs/libsmb2.so ../../bin/linux-x64/libsmb2.so

# export CC=x86_64-w64-mingw32-gcc 
# export CXX=x86_64-w64-mingw32-g++ 
# export CPP=x86_64-w64-mingw32-cpp 
# export RANLIB=x86_64-w64-mingw32-ranlib 
# export PATH="/usr/x86_64-w64-mingw32/bin:$PATH" 

# CC="x86_64-w64-mingw32-gcc" CXX="x86_64-w64-mingw32-g++" CPP="x86_64-w64-mingw32-cpp" RANLIB="x86_64-w64-mingw32-ranlib" PATH="/usr/x86_64-w64-mingw32/bin:$PATH" ./configure --without-libkrb5 --disable-dependency-tracking --host=x86_64-w64-mingw32