# KLM Assessment by Quintin Yu

In this assessment my Unity skills and knowledge are tested. For this assignment I have to use Unity Events, Scriptable Objects, Changes in Prefabs, a UI Canvas and TextMeshPro. During the creation of this project I have used AI (ChatGPT) to support me in the development to make my development cycle. Sadly I was still unable to complete all the tasks that were asked of me. For what I had planned, I will have an extra section down below.

## Youtube Video
As requested I have made a video for this project. This video is in done in Dutch. As I was unsure what I should show and talk about in the video I mainly showed the hierarchy view as well as the project and showed how the project currently works and runs. I quickly showed how I wanted to implement the requested requirements of landing and parking by manually adjusting values in the Inspector view.
https://youtu.be/Bcrks3xpc44

## Events and Scriptable Objects
I used Unity Events together with Scriptable Objects. By creating Unity Events through Scriptable Objects it was easy to add new commando's when they were necessary. This also allowed me to easily execute the events using a command sent through an imputfield as well buttons underneath said inputfield. 

## Change in Prefabs
I created a prefab for the main plane. This prefab is what I used as basis for all planes. As I changed the plane used for the prefab, I applied or reverted my changes where needed through the prefab override system. Other usages for prefabs would be to change the models of the airplanes, giving each plane a different look, while keeping the same logic making them behave in the same way.

## UI Canvas + TextMeshPro
In the UI I added two sections for extra controll. 

The first section is located in the bottom left. This section is used to give commands to the planes through either user input by using a command like /takeoff or by pressing the button with an ascending plane. When a command is used, the user will get feedback right above the inputfield stating what command has been given. After 5 seconds this will then dissapear. 

The second section on the bottom right, is used to switch between camera's of each airplane. As my main camera is not moving and is giving a limiting view, I decided to create a camera on each plane. These will give you a view from behind the airplane.

## Problems I faced
During the making of this project I quickly noticed that I haven't used Unity in a while. This did bring some set backs as I forgot how to use specific components. Unity also updated to Unity 6 since the last time I used Unity, removing methods such as rb.velocity and rb.drag. While I was able to find the replacements it did take extra time.

The biggest hurdle I faced however had nothing to do with Unity, and more with how I wanted to do this. I decided to make the plane work more based on physics. Meaning the plane needs to accelerate to a certain speed before it can take off. Tweaking the different settings, noticing too late that the model is rotated, and wanting to make the take off look good took a lot of time I could have spent better. Simply said I over complicated how I wanted to have the plane take off. With the plane flipping on its nose, moving too fast and being unable to turn quickly due to the physics I used for flight, I was unable to make the plane land and park in the correct hangar in time.

## What I had planned
As I mentioned I was unable to complete all tasks that were asked of me for this assignment. In this section I will go through the plans I had for the implementation of these subjects.

### Landing
While the plane is capable of taking off, it currently is incapable of landing. I was planning to have the plane turn around and aim at the runway it had to land on. I have 2 variables important for flight: Thrust and Lift. By lowering the Lift, the plane would slowly lose elevation until finally touching the ground. By lowering the Thrust I could make the plane come to a full stop.

By keeping the Thrust turned on the plane would be able to drive around, since the Lift would be set to 0 the plane would be incapable of taking off again.

### Parking
To park the planes there are 2 approaches I could've taken. 

The first one is by making using an empty game object as their destination and slowly move the plane towards that empty game object, stopping the plane once it reaches the game object. This would however cause the planes to go through everything or get stuck when hitting other planes, fences or the hangars.

The second approach would be by using NavAgents. It would keep the destination emtpy game object, but it would allow the planes to move while avoiding collisions with other planes as well as collision with buildings and fences.

### Scriptables for Plane information and Hangars
I wanted to have each planes information stored inside of Scriptable Objects. This way I could easily make new planes. I wanted to have the plane names and models connected through the scriptable objects. The plane names could than be coupled to the hangar. I would than have the correct plane name above the correct hangar. By editting the Scriptable Object, I would only have to change the name in one position, instead of having to change the name above the plane as well as above the Hangar.

## Github
Since I was working alone, I pushed everything to the main branch. The best practice would be to have a branch for each feature I am implementing. Though to save time I decided against doing this. I should also have committed more often to the repository in my own opinion. Something I only started doing later during the project.
