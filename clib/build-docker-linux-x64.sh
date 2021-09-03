#!/bin/sh

cd /build

echo "Updating apt..."
apt-get update > /dev/null 2>&1
# apt-get install -y vim
echo "Installing build dependencies..."
apt-get install -y gcc make automake autoconf git libtool > /dev/null 2>&1

echo "Running git clean..."
git clean -x -d -f > /dev/null 2>&1

echo "Running bootstrap..."
./bootstrap > /dev/null 2>&1

echo "Generating Makefile..."
automake > /dev/null 2>&1
echo "Generating configure file..."
autoreconf -i > /dev/null 2>&1

echo "Configuring..."
./configure --without-libkrb5 --disable-dependency-tracking > /dev/null 2>&1
echo "Building..."
make > /dev/null 2>&1