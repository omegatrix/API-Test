# API-Test

### Test API which pulls user data from another API and returns a list of users who are listed as either living in London, or whose current coordinates are within 50 miles of London.

**Path - /WithinLondon**

*1. The approach is to get the users who are listed as living in London by perfoming a GET request specifying the "city" parameter*.
*Next step is to get all the user from the API*.

*2. Now need to find out which coordinates fall within the distance of 50 miles from London. For this I've used Haversine Formula to calculate the distance between two points*. 

*3. Given that Latitude and longitude coordinates of London are: 51.509865, -0.118092, loop through each user pass their coordinates to determine if the distance between london coordinates and user coordinates is within 50 miles*.

*4. Finally merge the users who are listed as living in London and users whose coordinates are within 50 miles of London and return the result*.

#### Other Get requests implemented as follows:

  **/AllUsers - returns all the users**
  
  **/User/{id} - returns a user by id**
  
  **/Users/{city} - returns the users who are listed as living in the specified city**
