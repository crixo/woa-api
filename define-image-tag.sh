#!/bin/bash
TRAVIS_TAG=
IMAGE_TAG=latest
#cd backend-aspnetcore2.0
#cd .. && echo $PWD
[  -z "$TRAVIS_TAG" ] && IMAGE_TAG_TMP=latest || IMAGE_TAG_TMP=$TRAVIS_TAG
IMAGE_TAG=$IMAGE_TAG_TMP
echo $IMAGE_TAG