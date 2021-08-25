# Pumpkin
<h4>Create Core Service using Asp.net 5.0</h4>

This project includes the capabilities of a Caching(redis and Inmemory Cache), logging with Elasticsearch and Kibana and Dapper Data Provider and etc...

How to Install <b>Redis</b> by Docker on Ubuntu/Debian:

  <code> sudo docker run --rm --name redis -p 6379:6379 redis </code>

How to Install <b>ElasticSearch</b> by Docker on Ubuntu/Debian:

  <code> sudo docker run -d --name elasticsearch -p 9200:9200 -p 9300:9300 -e "discovery.type=single-node" elasticsearch:7.9.2 </code>
  
How to Install <b>Kibana</b> by Docker on Ubuntu/Debian:

  <code>sudo docker run -d -p 5601:5601 -h kibana --name kibana --link elasticsearch:elasticsearch kibana:7.9.2</code>
