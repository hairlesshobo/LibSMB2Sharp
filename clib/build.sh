#!/bin/sh

# arch=$(uname -i)
arch=x64

mkdir -p bin

rm -fr src
mkdir -p src
cd src

echo "Cloning repo..."
git clone https://github.com/sahlberg/libsmb2.git > /dev/null 2>&1

cd ../../

echo "Starting to build libsmb2-linux-${arch}.so in docker..."
docker rm build-libsmb2-linux-${arch} > /dev/null 2>&1
docker run --name build-libsmb2-linux-${arch} -v $(pwd):/build -it debian:stretch /build/build-docker-linux-x64.sh 

cp lib/.libs/libsmb2.so  ./bin/libsmb2-linux-${arch}.so



# if [ "$1" = "clean" ]; then
# 	echo "Cleaning up..."
# 	rm -fr src
# fi
