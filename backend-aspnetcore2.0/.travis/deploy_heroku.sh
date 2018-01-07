# https://docs.travis-ci.com/user/deployment/heroku/
#!/bin/sh
# wget -qO- https://toolbelt.heroku.com/install-ubuntu.sh | sh
# heroku plugins:install heroku-container-registry
# docker login -e _ -u _ --password=$HEROKU_AUTHTOKEN registry.heroku.com
# heroku container:push web --app $HEROKU_APP_NAME

#- heroku plugins:install heroku-container-registry
#- export TAG=$REPO-heroku:$COMMIT
docker build -f heroku.Dockerfile -t $TAG .
docker tag $TAG registry.heroku.com/woa-api-sample/web
docker login --username=$HEROKUU_USERNAME --password=$HEROKU_AUTHTOKEN registry.heroku.com
docker push registry.heroku.com/woa-api-sample/web