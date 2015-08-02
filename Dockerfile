FROM mono

EXPOSE 5000

RUN mkdir /usr/owinswagger
ADD . /usr/owinswagger
WORKDIR /usr/owinswagger

RUN nuget restore
RUN xbuild

CMD [ "mono", "/OwinSwagger/bin/Debug/OwinSwagger.exe" ]
