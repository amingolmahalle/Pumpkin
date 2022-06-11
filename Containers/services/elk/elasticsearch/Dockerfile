FROM docker.elastic.co/elasticsearch/elasticsearch:7.14.0

ENV cluster.name=elk-dev-cluster
ENV ES_JAVA_OPTS="-Xms512m -Xmx512m"
ENV bootstrap.memory_lock=true
#RUN sysctl -w vm.max_map_count=262144
#RUN echo vm.max_map_count=262144 >> /etc/sysctl.conf
EXPOSE 9200 9300