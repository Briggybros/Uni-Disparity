#! /bin/sh

project="disparity"

echo "Attempting to build $project"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -quit \
  -username Briggybros \
  -password Disparity4eva \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -executeMethod Build.PerformBuild

echo 'Logs from build'
cat $(pwd)/unity.log

ls $(pwd)
ls $(pwd)/Build/
ls $(pwd)/Build/android/
