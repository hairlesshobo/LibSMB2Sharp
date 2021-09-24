#!/bin/sh
# requirements
#   - dotnet
#   - xmldoc2md (https://charlesdevandiere.github.io/xmldoc2md/)
#   - dotnet tool install -g XMLDoc2Markdown


# SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"
SCRIPT_DIR="$( cd "$( dirname "$0" )" && pwd )"

cd $SCRIPT_DIR/../

cp ./Directory.Build.props ./docs/build_props.xml

#dotnet build 

# cd ./docs
#xmldoc2md ../LibSMB2Sharp/bin/Debug/netstandard2.0/LibSMB2Sharp.dll ./api/ --examples-path examples/

# here we will need to generate the api/_sidebar.md file from the input index.md file
#cd ./api
#cat ../_sidebar.md index.md \
#    | grep -v -e '^$' \
#    | grep -v -e '^# LibSMB2Sharp$' \
#    | sed 's/##/    -/' \
#    | sed 's/#/    -/' \
#    | sed 's/^\[/        - [/' > _sidebar.md
