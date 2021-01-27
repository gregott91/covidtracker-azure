# covidtracker-azure

This repository defines 2 Azure Functions:

* A timed function, scheduled hourly
* A HTTP trigger function

Both functions execute the same behavior. First, they reach out to an Azure Devops Pipeline to grab the latest artifact - in this case, an executable which generates a JSON file of COVID data. Then, the pipelines run the executable. Finally, the pipelines commit the COVID data via git to a GitHub pages repo, which is then used to generate a COVID data website.

That website can be found here: https://gregott91.github.io/CovidTracker/index.html