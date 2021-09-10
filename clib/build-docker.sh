#!/bin/sh
#
#  LibSMB2Sharp - C# Bindings for the libsmb2 C library
# 
#  Copyright (c) 2021 Steve Cross <flip@foxhollow.cc>
#
#  This program is free software; you can redistribute it and/or modify
#  it under the terms of the GNU Lesser General Public License as published by
#  the Free Software Foundation; either version 2.1 of the License, or
#  (at your option) any later version.
#
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU Lesser General Public License for more details.
#
#  You should have received a copy of the GNU Lesser General Public License
#  along with this program; if not, see <http://www.gnu.org/licenses/>.
#

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