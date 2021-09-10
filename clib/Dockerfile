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

FROM debian:stretch

RUN apt-get update \
  && apt-get install -y \
    autoconf \
    automake \
    gcc \
    git \
    libtool \
    make \
  && rm -rf /var/lib/apt/lists/*

# RUN apt-get update \
#     && apt-get install -y mingw-w64 \
#     && rm -fr /var/lib/apt/lists/*