# NET 5 + MongoDB + Graphql Session Example
This is a example that shows a proof of concept of a session service with MongoDB.

You can use docker to start a instance of Mongo locally.
```
docker run -d -p 27017-27019:27017-27019 --name mongodb mongo
```

Example data: 
```graphql
mutation {
   createSession(
     input: {
       sessionStates: [
         { key: "language", value: "es"}
         { key: "test", value: "yes"}
       ]
     }
   ){
     session {
       state{
         key
         value
       }
       id
     }
   }
}
```