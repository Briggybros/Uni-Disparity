#! /bin/sh
echo 'Downloading from http://beta.unity3d.com/download/ee86734cf592/unity-editor_amd64-2017.2.0f3.deb: '
curl -o unity.deb http://beta.unity3d.com/download/ee86734cf592/unity-editor_amd64-2017.2.0f3.deb

echo 'Installing Unity'
sudo apt install ./unity.deb -y

echo 'Removing installer'
rm unity.deb
