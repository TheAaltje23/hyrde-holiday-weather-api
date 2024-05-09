
# Hyrde Candidate Challenge

##### Your very own weather display application

## Description
Welcome and thank you for your interest in joining the Hyrde IoT development team. We’ve designed a short assignment for you that will help you to show Hyrde which professional skills you possess. The assignment which you are going to make will revolve around both frontend and backend development and has room for you to showcase your creativity and demonstrate the way you work.
The goal of this assignment is not to be an exam where you will either pass, or not. Instead this assignment is meant for you to implement a few requirements in the way you see fit. There are a few limitations of course, as you will be bound to the tech stack that Hyrde uses for their projects but you will find that the assignment has plenty of space for creativity and we encourage you to fill in some of the blanks. 


## Assignment
The Hyrde development team wants to go on a holiday and is looking for a nice location that they see fit.  There’s only one problem. Nobody can think of the perfect place. Pieter seems to like the Netherlands, Robin wants to go to Belgium, Rene and Berke want to go down south and Maikel can’t make up his mind at all. What they can agree on is that the weather is most important. This is where you will assist. 

Knowing that you’re the man of the job, the team has asked you to build a small website that they can use to check the weather in any city that they are interested in. They would like to see a website which can be visited on both mobile devices and their laptops. When they visit the website they would like to be greeted by a webpage where they see a small form with a textbox in which they can fill in the name of a city. When they submit the form, the website will send a request to the API which will return a weather forecast. Because the team wants to escape the hectic work life, they want to travel for a few days. 

Fortunately the team already prepared a little bit of work for you. You will find a backend (.NET) project as well as a frontend (Quasar) project.

### Backend
Extend the backend project with the following:
1.	Interface with an external Weather API such as WeatherApi or OpenWeatherMap.
2.	Extend the WeatherController using the WeatherService by implementing the Forecast and Today methods. Make sure that both endpoints return the Weather model. You can modify the model as you see fit.
3.	Make sure that the endpoints can be consumed by the frontend.
4.	Make sure that you have sufficient retry action if the service is unavailable.

### Frontend
Extend the frontend project with the following:
1.	Build a frontpage that shows a form where a user can fill in a city name. When submitting the form the user is redirected to a new page where an overview of  the weather forecast. The table needs to show at least the temperature in Celsius and the weathertype (rain, sunny, etc)
2.	After submission the weather overview page shows the weather for the oncoming few days, in a table which can be sorted by columns and filtered by searching. You can use the Quasar table component for this job.

### Extra:
1.	To improve sustainability of your code, tests are strongly recommended. (Try to) Implement tests that fit your implementation.
2.	If you have time left, feel free to add anything you like to showcase your skills. Anything goes. 

### Check in your code
Are you ready with your assignment? Great! You can check in the code to Azure DevOps.
You'll have to create a new branch and submit a pull request. During your second interview we'll go over the work that you have done. 

## Resources and requirements

For this assignment you can use any tools you wish.