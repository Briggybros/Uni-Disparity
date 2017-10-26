#! /bin/sh

echo "Attempting to build disparity"
/opt/Unity/Editor/Unity \
  -batchmode \
  -force-free \
  -quit \
  -nographics \
  -username DisparityGame \
  -password Disparity123 \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -buildTarget android \
  -executeMethod Build.PerformBuild

cat unity.log

echo "Is the apk there?"

ls Build/android