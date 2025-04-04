# Datrix technical challenge

Hi!

Thanks again for applying to Datrix, and take a minute to pat yourself on the back for getting to this stage of the interview process! Your submission to this technical challenge will be a significant contributing factor to the success of your application.

Please see the following sections for the detailed instructions, and good luck!

Lazar, Sam and the Datrix Engineering Team

## Instructions

You’ll find **two folders** in the provided zip file. These two folders represent **two independent tasks**. 

**You are required to complete only ONE of the two tasks.** Choose the task you wish to complete, and modify the content of the relevant folder directly as you work to solve it.

Overall:
* Use **additional libraries and reference material** as you wish.
* Unless explicitly discouraged, feel free to optimize your submission in any way you feel is important.
* In the interest of fairness, please **refrain from asking us questions**. If you do find yourself unsure about something, use your common sense to make an assumption, and clearly state it in an obvious place in your submission (e.g. a README file).
* In the interest of fairness, we’ll be anonymizing your submissions for the “grading” stage. Please **do NOT include your name (or other identifying information) anywhere in the actual content (“source”) of your submission**.

The specific instructions for the two tasks are listed in the sections below.

### Submission

When you’re ready to **submit**, please remove the build artefacts (i.e. delete `node-modules` and `dist` from the *colorful* folder and/or "clean" the *FisherYates* build), remove the folder of the task that you chose *not* to do, re-zip and upload it to a cloud-storage folder of your choice (e.g. Google Drive). The name of the zip should be in the format: yourfirstname.zip (e.g. `sally.zip`). Once you have the link to your cloud ZIP file, submit it by responding to the technical assessment email that contained the instructions PDF. Your email response should include the link to your cloud ZIP file. Note: Please don't attach the zip file to your email email directly (our spam filters might red-flag your message if you do).

---

## Task 1: *Colorful*

This task focuses on concurrency in [Ember.js “Octane”](https://emberjs.com/) (the modern JS framework on which Datrix's production client single-page app (SPA) is built).

The *colorful* folder contains a simple Ember Octane app called *Colorful*. It's just a bare-bones `ember-cli@4.9.2` app (created with the command `ember new colorful`) with [Bootstrap](https://getbootstrap.com/) added for some basic styling. Importantly, the [Ember Concurrency](http://ember-concurrency.com/docs/introduction/) add-on has been added already to help you solve the task.

*Colorful* is an app that allows the user to change the color of a static page heading. All new users start with the default color `#BADA55`.

The user can then opt to change the color by typing the desired HEX code directly into the text field. The user should then see the color update automatically without having to click any kind of *Save* button. 

*Colorful*'s business model revolves around selling "Pro" subscriptions. The user can only use a custom color if they're subscribed to "Pro". To reduce the initial upgrade friction, a free 30-day "Pro" trial (which includes custom colors) is automatically started when the user first changes the color away from the `#BADA55` default. The user receives a *"Trial started"* email when their trial has started.

To achieve the auto-update and auto-trial-start behavior, you'll see that a (junior) *Colorful* developer has hooked up the text-field's `keyup` event to fire a `PATCH /accounts/<user-id>` call to the server. 

The `PATCH /accounts/<user-id>` server endpoint, which you can assume is already fully implemented, does the following:
1. If the requested color isn't `#BADA55` and the account hasn't yet started a *Pro* trial, it starts a trial and saves the relevant trial information to the database for reference. The trial is started by creating a new "Customer" and "Subscription" in an external subscription system (which also sends a *"Trial started"* email). This external API call typically takes "relatively long" (a few seconds) to complete.
2. Update the color in the database to the requested color.

In the code you've received, the above server behavior is "mocked/simulated" to return a 200 status after a few seconds (the delay reflects the possible call to the external subscriptions-API). You'll see these calls logged in the DevTools console when you type into the text field.

The *Colorful* team was under time pressure and deployed the version they had. Various issues have now started to surface:
1. The ops team has noticed that there are far too many requests hitting the server, which is causing back-end performance issues.
2. New customers are complaining because they receive numerous identical *"Trial started"* emails in their inbox after they register and change their color.
3. Customers are complaining because the text field sometimes "has a mind of its own": It starts "typing in characters itself"! 

### Your task

Carefully investigate the reported bugs and refactor the Ember app to resolve them.

**Do:**
* Modify the `my-profile.js` component code that's responsible for handling the user interaction and server calls.
* Help protect the server by preventing the client from sending a request every time the user hits a key.
* Ensure that the text field never "has a mind of its own" (as described above).
* Ensure your solution won't cause users to receive multiple *"Trial started"* emails (to be clear, this translates to "ensure that only a single request is 'in-flight' to the server at any given time").
* Ensure that the "last typed" color is always saved to the server (eventually) when the user stops typing, irrespective of the timing involved in their typing, and without the user having to manually type again to force a "sync/update" in any way.
* Use the Ember Concurrency documentation as required.

**Do NOT:**
* Do **not** change the way the user provides the input (it must remain a text field).
* Do **not** change the way the mocked server behaves.
* Do **not** try to implement any of the server logic.
* Do **not** try to create any "subscription"- or "email"-relevant structures/logic yourself (you can assume that's all handled by the server code as part of the `PATCH /accounts/<user-id>` call).
* Do **not** worry about client-side and server-side validation. That is, you can assume that users will always enter valid HEX values.
* Do **not** worry about the case where the user is interacting with the app concurrently from multiple devices/browser tabs. Assume all interaction is done in a single browser tab from a single device.
* Do **not** worry about implementing a loading spinner or similar progress indicator (the console logs are enough for this task).
* Do **not** worry about writing automated tests.
* Do **not** worry about making the UI more "pretty" than it currently is.

---

## Task 2: *Fisher-Yates*

This task focuses on code design and unit-testing for a non-deterministic algorithm.

The *FisherYates* folder contains a bare-bones [ASP.NET Core 7.0](https://learn.microsoft.com/en-us/aspnet/core/getting-started/?view=aspnetcore-7.0&tabs=windows) solution with a "Web app" project and a supporting "Unit test" project. 

The [*Fisher-Yates shuffle*](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle) is a classic and simple algorithm for shuffling an input sequence of elements. The  [modern version of the algorithm](https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm) (either direction) is the focus of this task.

The provided app has a single endpoint for this algorithm, `GET /fisheryates`, defined as follows:

* **Input:** A string, containing the dasherized list of elements to shuffle (e.g. "D-B-A-C").
* **Response:** A `200 OK` HTTP response with a content-type of `text/plain; charset=utf-8`. The content should be the dasherized output of the algorithm, e.g. "C-D-A-B".

### Your task

In the web application project, add code structures that would support the implementation of the modern version of the algorithm linked above. The code structures should be in **skeleton form only** for holding the algorithm's logic. All method bodies should simply throw a new `NotImplementedException`. 

Then, implement **unit tests only** in the unit tests project to test the algorithm.

**Do:**
* Structure the skeleton for the solution in the web app project in a way that you feel follows best-practices for a modern Asp.Net Core 7.0 web application.
* Write unit tests for correctness, to the extent that you feel is appropriate and follows best-practices. The testing approach should address the issue of non-determinism ("randomness").
* Feel free to use any libraries (e.g. from NuGet) if you feel they'd help you.

**Do not:**
* Do **not** actually implement the algorithm. **You only need to build the skeleton structure and write the unit tests**. All your unit tests should be failing in your submission (a la [TDD](https://en.wikipedia.org/wiki/Test-driven_development) and its red-green development cycle).
* Do **not** worry about server-side validation (assume the inputs are always valid).
* Do **not** implement any kind of end-to-end/acceptance/Selenium/browser testing (i.e. just write the unit tests).
* Do **not** implement any kind of performance testing or similar (i.e. just focus on unit testing for correctness).