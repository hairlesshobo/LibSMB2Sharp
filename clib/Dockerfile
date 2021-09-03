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