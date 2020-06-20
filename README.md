# Pumpkin
<h4>Create Core Service using Asp.net Core 3.1</h4>

How to Install <b>Redis</b> by Docker:

  <code> sudo docker run --rm --name redis -p 6379:6379 redis </code>

How to Install <b>ElasticSearch</b> by Docker:

  <code> sudo docker run -d -p 9200:9200 -p 9300:9300 -it -h elasticsearch --name elasticsearch elasticsearch:7.6.2 </code>
  
How to Install <b>Kibana</b> by Docker:

  <code>sudo docker run -d -p 5601:5601 -h kibana --name kibana --link elasticsearch:elasticsearch kibana</code>
