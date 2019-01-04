FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithASPNetCoreUdemy/db/migrations/ /home/database/
COPY ./RestWithASPNetCoreUdemy/db/dataset/ /home/database/
COPY ./RestWithASPNetCoreUdemy/ci/init_database.sh /docker-entrypoint-initdb.d/init_database.sh