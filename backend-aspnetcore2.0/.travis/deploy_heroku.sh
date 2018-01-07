# https://docs.travis-ci.com/user/deployment/heroku/
#!/bin/sh
wget -qO- https://toolbelt.heroku.com/install-ubuntu.sh | sh
heroku plugins:install heroku-container-registry
docker login -e _ -u _ --password=$HEROKU_AUTHTOKEN registry.heroku.com
heroku container:push web --app woa-api-sample

#- heroku plugins:install heroku-container-registry
#- export TAG=$REPO-heroku:$COMMIT
#docker build -f heroku.Dockerfile -t $TAG .
#docker tag $TAG registry.heroku.com/woa-api-sample/web
#docker login -e _ -u _ --password=$HEROKU_AUTHTOKEN registry.heroku.com
#docker push registry.heroku.com/woa-api-sample/web