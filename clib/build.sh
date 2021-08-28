#!/bin/env sh

version="3.0.0"
arch=$(uname -i)

rm -fr src
mkdir -p src
cd src
echo "Downloading source..."
wget https://github.com/sahlberg/libsmb2/archive/refs/tags/v${version}.tar.gz > /dev/null 2>&1
echo "Extracting..."
tar zxf v${version}.tar.gz
cd libsmb2-${version}
echo "Generating Makefile..."
automake > /dev/null 2>&1
echo "Generating configure file..."
autoreconf -i > /dev/null 2>&1
echo "Configuring..."
./configure --without-libkrb5 > /dev/null 2>&1
echo "Building..."
make > /dev/null
cp lib/.libs/libsmb2.so.${version} ../../../lib/libsmb2-${arch}.so.${version}
echo "Cleaning up..."
cd ../../
rm -fr src
