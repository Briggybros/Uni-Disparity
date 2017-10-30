#! /bin/sh

/opt/Unity/Editor/Unity \
  -batchmode \
  -force-free \
  -quit \
  -username greg.sims.2015@bris.ac.uk \
  -password Disparity123 \
  -nographics \
  -logFile $(pwd)/unity.log \
  -projectPath $(pwd) \
  -buildTarget android \
  -executeMethod Build.PerformBuild

cat unity.log

echo "Is the apk there?"

ls Build/android