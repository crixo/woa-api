#!/bin/bash
#TRAVIS_TAG=
#IMAGE_TAG=latest
#cd backend-aspnetcore2.0
#cd .. && echo $PWD
[  -z "$TRAVIS_TAG" ] && IMAGE_TAG=latest || IMAGE_TAG=$TRAVIS_TAG
echo $IMAGE_TAG