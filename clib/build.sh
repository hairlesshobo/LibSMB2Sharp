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

# arch=$(uname -i)
arch=x64

mkdir -p bin

rm -fr src
mkdir -p src
cd src

echo "Cloning repo..."
git clone https://github.com/sahlberg/libsmb2.git > /dev/null 2>&1

cd ../

echo "Build docker build image..."
docker build --tag libsmb2-build:latest .

echo "Starting to build libsmb2-linux-${arch}.so in docker..."
docker rm build-libsmb2 #> /dev/null 2>&1
docker run --name build-libsmb2 -v $(pwd):/build --user $(id -u):$(id -g) -it libsmb2-build:latest /bin/bash #/build/build-docker.sh 
# docker rm build-libsmb2 > /dev/null 2>&1

# cp lib/.libs/libsmb2.so  ./bin/libsmb2-linux-${arch}.so



# if [ "$1" = "clean" ]; then
# 	echo "Cleaning up..."
# 	rm -fr src
# fi
