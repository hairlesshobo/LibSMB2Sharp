#!/bin/sh

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
