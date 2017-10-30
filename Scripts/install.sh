#! /bin/sh
echo 'Downloading from http://beta.unity3d.com/download/ee86734cf592/unity-editor_amd64-2017.2.0f3.deb: '
curl -o unity.deb http://beta.unity3d.com/download/ee86734cf592/unity-editor_amd64-2017.2.0f3.deb

echo 'Installing Unity'
sudo apt install ./unity.deb -y

echo 'Removing installer'
rm unity.deb

echo 'Downloading license'
mkdir -p ~/.local/share/unity3d/Unity
curl -o ~/.local/share/unity3d/Unity/Unity_lic.ulf https://www.dropbox.com/s/3fm8vv3q6ngd2jg/Unity_lic.ulf
