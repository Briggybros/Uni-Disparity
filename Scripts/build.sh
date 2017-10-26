#! /bin/sh

project="disparity"

echo "Attempting to build $project"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -quit \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -executeMethod Build.PerformBuild

echo 'Logs from build'
cat $(pwd)/unity.log
