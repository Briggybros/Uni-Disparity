#! /bin/sh

project="disparity"

echo "Attempting to build $project"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -buildTarget Android "$(pwd)/Build/android/$project.apk" \
  -quit

echo 'Logs from build'
cat $(pwd)/unity.log
