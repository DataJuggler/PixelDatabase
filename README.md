# PixelDatabase

2.14.2025: System.Drawing.Common library was updated to 9.0.2.

11.22.2024: This project has been updated to .NET 9.

11.5.2024: I fixed a bug where the CreateNew was not setting the background image. This was
caused by when I added a default Criteria of Alpha > 0 so most queries only affect the visible part of an image.
This also fixes the Show query.

    // Simple Show query, will set the alpha value to 255 for all pixels
    string query = "Show";"

	// Update
	PixelQuery pixelQuery = pixelDatabase.ApplyQuery(query, null);

	// test the results
	int pixelUpdated = pixelQuery.PixelsUpdated;

11.3.2024: I added a new method to PixelDatabase called CreateNew. This creates a new 
Bitmap and loads a pixeldatabase for the Width and Height givne.
I also added a new DrawLine method to simplify Line drawing.

7.24.2024: RandomShuffler and UltimateHelper were updated.
I also added a new method Clone, whiich creates a copy of a PixelDatase. Untested.

2.28.2024: I moved the Title of each calendar up 8 pixels. 

2.27.2024: I now draw a line between Header and Day Roy Image. If no Day Row Image, it is still a horizontal line
1 vertical pixel below the header.

2.26.2024: I changed NuGet PixelDatabase.CreateCalendar to accept a BaseFont and a HeaderFont. It is a breaking
change, but I figure now is the time to break this method since it is new.
Also added an optional parameter as of a pixel database for the day row image. This is the row where the
day names are abbreviated.

2.23.2024: I added a new method to PixelDatabase - CreateCalendar, which takes in an argument for a Month and a Year.
View the demo project here: https://github.com/DataJuggler/CalendarCreator

2.16.2024: PixelDatabase.DrawLine and DrawRepeatingLine were updated. A Bitmap is now returned from the methods, so you can 
get the updates that were applied. 

12.7.2023: New Properties - MaxBlueDifference, MaxGreenDifference, MaxRedDifference. This allows you to query on 
positive numbers, verse RedMaxDifference made you work with negative numbers and it is a little unnatural feeling.

11.22.2023: UltimateHelper was updated.

11.18.2023
Version 8.0.0: This project has been updated to .NET Core 8.0.0.

7.5.2023: I added a Rescale image, which returns the dimensions for a new image by passing in a
MaxWidth and MaxHeight value, and the code determins the ratio by taking the lowest of ratio x
and ratio y. To actually resize the image, call Resize. I am using this new method in 
DataJuggler.BlazorGallery project for the FullScreenImageViewer component.
Live sample (https://blazorgallery.com).

4.24.2023: I found a better method of resizing an image, and wrote a new Resize method that returns
a loaded PixelDatabase at the new size.

Update 4.23.2023:
The current version is 7.0.7. I added a Resize method which returns a PixelDatabase loaded with the new size.
There was already a ResizeImage, but this method makes it easier for use with PixelDatabase.

PixelDatabase.Net is now live: 
https://pixeldatabase.net
A Free Online Text Based Image Editor

I am starting a batch version of this project now because I need it for videos.

Please visit my YouTube channel, as I make videos for PixelDatabase.Net as often as I can:
https://www.youtube.com/channel/DataJuggler

This class library and Nuget package was built as part of my project TransparencyMaker. I created this library so I can build a Blazor version for a domain I just bought today called PixelDatabase.

The documentation below is copied from Transparency Maker.

Here is the link to TransparencyMaker:

https://github.com/DataJuggler/TransparencyMaker

Most of this is applicable to this project now that it has been ported to a class library, I just copied and pasted for now to save time.
I apologize in advance, but this is better than none.

# Motivation
The reason I wrote Transparency Maker originally is when I purchase stock photos, almost all come with a background color and often I need a transparent background. There are other tools that perform background removal, with more now than in 2012 when I started working on Transparency Maker, however I wanted a way to programmaticaly manipulate pixels.

# Rebranding the name to Pixel Database (done for the Nuget package)
The original purpose of Transparency Maker was to make backgrounds transparent, thus the name. I have now expanded the features to include updating pixels, changing colors, etc. 

I am continuing testing, the rest of these docs use caution some are specific to the Windows Form app, and some are in the class library / Nuget package DataJuggler.PixelDatabase.

All the functionality that can be done in TransparencyMaker can be done in PixelDatabase, except for Set Background Color, that is specific to Transparency Maker. Updated documenation coming soon.

# Visual Studio 
Visual Studio 2019 is required, version 16.4+.

# Transparency Maker Videos on my Channel (new videos coming as soon as today)

https://www.youtube.com/playlist?list=PLKrW5tXCPiX2PxrLPszDzlcEZwQG-Qb8r

I just updated my channel to be split into more play lists to separate each section.

# How PixelDatabase Works
The first thing that happens when an image is opened, is your file is parsed into a list of PixelInformation objects, aka Pixel Database.

When the program starts, click the Start button to select your image. The image must be a Png or a Jpg.

<img src="https://github.com/DataJuggler/TransparencyMaker/blob/master/Docs/ScreenShot.png">

A Pixel Database is a List of PixelInformation objects that contain properties about the pixel. To find the pixel information for any pixel, turn the Color Picker on.

<img src="https://github.com/DataJuggler/TransparencyMaker/blob/master/Docs/ColorPicker.png">

Tip: To determiine if the Color Picker is on, hover your mouse of any part of your image. If the mouse cursor turns into a pointer (a hand), then the Color Picker is on. 

Once the Color Picker is turned on, click any pixel in your image and a Pixel Information Box pops up:

<img src="https://github.com/DataJuggler/TransparencyMaker/blob/master/Docs/PixelInfoBox.png">

The box contains the following properties:

# Red
The Red value of RGB value. Value range is 0 - 255.

# Green
The Green value of RGB value. Value range is 0 - 255.

# Blue
The Blue value of RGB value. Value range is 0 - 255.

# Alpha
The Transparency Level of the pixel. Values are 0 - 255, where 0 is transparent and 255 is visible.

# X
The horizontal position of the pixel clicked. 

# GreenRed
The sum of Green plus Red.

# BlueGreen
The sum of Blue plus Green.

# BlueRed
The sum of Blue plus Red.

# Total
The sum of Red plus Green plus Blue.

# Y
The vertical position of the pixel clicked.

You can use all of the above values in your queries to manipulate your image.

# BQL - Bitmap Query Language

# Why BQL?
This application started out as a plain Windows Form that I would hard code C# statements to replace the background of stock photos with transparent backgrounds. Over time I switched to a more object oriented approach where I analyze the entire image, and thus the Pixel Database was created.

The initial criticism I have received is there are not any controls like most graphic programs. If that is what everyone still wants I am open to ideas about what types of controls are needed. I created BQL because it is similar to SQL that many C# developers already know, thus enabling them to get up to speed quickly. Another reason I created BQL instead of a control based graphical UI is time to develop. Many of my failed endeavors have involved very complicated UI's, and then I am reminded of this small company called Google that started with a webpage that contained a single textbox and a button. 

# Hide / Show vs Update Queries

There are two types of queries; Hide/Show and Update.

# Hide / Show

# Hide
Any pixels affected by the query will be set to Alpha 0, which makes them invisible.

# Example
Hide Pixels Where  
Total > 700

# Shortcut: Hide
Hide Pixels Where can be shortened to:

Hide<br/>
Total > 700 

# Show
Any pixels affected by the query will be set to Alpha 255, which makes them visible.

# Example
Show Pixels Where  
Red < 200

# Shortcut: Show
Show Pixels Where can be shortened to:

Show<br/>
Red < 200

# Important
This query parser may be rewritten to be more robust someday, but for now certain attributes must be on their own line.
I wrote the query parser in a few hours many years ago, and it works pretty well so I have not bothered to refactor it (yet).

So the query above would fail if written like this

# Invalid
Show X < 150

# Correct
Show<br/>
X < 150

It wouldn't be that hard to rewrite the query parser, so let me know if you think the line by line requirement is confusing. I know it is crude, but this app was designed to be quick, dirty and functional at first.

# Operators

# Equals
# Equals Symbol: =
Will match criteria on exact values.

# Equal Example: 

Hide<br/>
Red = 233

All pixels that have a Red value of 233 will be set to Alpha 0 to be hidden.

# Greater Than
# Greater Than Symbol: >

Will match criterian on greater than or equal to

# Greater Than Example

Show<br/>
Y > 1200

All pixels with a Y value of 1,200 or higher wil be set to Alpha 255 to be shown.

# Less Than
# Less Than Symbol: <

Will match criteria on less than or equal to

# Less Than Example

Hide<br/>
BlueGreen < 300

All pixels with a BlueGreen value of 300 or less wil be set to Alpha 0 to be hidden.

# Between

Will match criteria that is greater than or equal to the first number and less than or equal to the second number.

# Between Example

Hide<br/>
Blue Between 200 255

All pixels with a Blue value between 200 and 255 will be hidden.


# Compound Statements

You can combine criteria to further narrow down the pixels that are manipulated

# Important: Each criteria must be on its own line.

# Update Queries
Update queries are very similar to Hide queries, except that you must include a Color attribute

# Set Color
There are two ways to set a color, Named Colors or RGB values.

# Named Color
You can set a pixel to a named color

Note: For a list of Dot Net Colors see System.Drawing.Colors or this website lists them:
http://www.flounder.com/csharp_color_table.htm 

# Named Color Example

Update<br/>
Set Color FireRed<br/>
Where<br/>
Total Between 125 150<br/>

# Note: Where must be on its own line for Update queries

# RGB Color
You set the color by specifying the Red, Green and Blue values

# RGB Color Example

Update<br/>
Set Color 121 220 7<br/>
Where<br/>
Y > 100<br/>

# Where is not optional

You must specify the Where, even if you want to update all pixels

Update<br/>
Set Color White<br/>
Where<br/>
Total > 0<br/>

# Note: In the above example, all pixels will be set to white, since greater than > is equal to > or equal to.

# New Feature: 2.22.2020 - Added Adjust color feature

The adjust color feature lets you adjust 1 color (Red, Green or Blue) or all colors by a certain amount.

Example: 
Update
Set Adjust Red 25
Where
Total > 0 

# Negative Color Example

The Adjustment value can also be negative.

Update
Set Adjust All -15
Where
Total > 0 

All pixels will be adjusted by 15, so Red = 33, will become red = 18.

It is safe to apply a value as if a color channel of a pixel goes over 255 the value will be 255, or below 0 will be 0.

# New Feature 2.23.2020: Swap (color)

I added a new useful feature called Swap, that let you replace the value of Red With Blue, Red With Green or Green With Blue, and of course the opposite of swap is the same; green to blue is the same as blue to green.

# Swap Example:

Update
Set Swap Red Blue
Where
Total > 0

The above query will replace the value of Pixel.Red with Pixel.Blue for all pixels in the image (again greater than > is the same as greater than or equal in BQL.

Swap is safe to use, in terms of if you have a pixel with a vlue of blue 30, and you subtract 50, the resulting value of blue will be zero, not -20. The same is true for adding over the max value of a color (255), the value will be set to 255.
One problem with swap and values going out of bounds is the contrast created by slight variances give an image a different look, compared to all pixels being the same color. Some information in your image will be lost if you go below zero or above the maximum for many pixels.

# New Feature 2.23.2020: Masks

Masks is something I have thought about for a long time. Today I worked on a sample and some unwanted pixels kept getting modified with my query, so I got motivated and created a new property to the PixelInformationObject called Mask.

Mask Rules (This is the my guide for programming them, and is created as I code this).

Masks will stay on after being created until you load a new image, remove the mask, clear all masks or close the program.

Masks only affect Update queries for now. Hide Pixel and Show Pixel and draw line queries are not getting this feature (yet). If it works well I may apply it to the other areas later.

# Mask Specification

Set Mask Verb Name

# Verbs 

Verbs are actions that can be performed to create, add, remove or replace masks.

# Add

Add a mask and give it a name.

Example: Set Mask Add Interior

# Replace

Replacing a mask removes all pixels affected by a mask, then creates a new mask for the pixels affected.
Example: Set Mask Replace Mailbox

If any pixels had a Mask applied named Mailbox, they would be removed, and then a new mask named Mask Mailbox is created for the pixels affected in the update query.

# Clear

Remove a mask by name.

Example: Set Mask Clear Fence.

# Clear (All)

To remove all masks, pass in the name All.

Set Mask Clear All

Note: Clear and Clear All may require you to still pass in a valid where clause.
If yes, You can use:

Update
Set Mask Clear Curb
Where
Total > 0

When I start testing this I will try and document what is actually expected, and if possible I would like to get rid of the required Where clause. It should mean everything from the Select like SQL. 

# Disabling a Mask

If you want to keep a mask on an image, but want to run another query where the mask isn't active (disabled), you can run this.

Set Mask Disable Bookshelf

To enable the query again:

Set Mask Enable Bookshelf.

# Hiding / Showing a Mask
By default, Masks are not visible, but a query to show them is as simple:

Show A Mask:

Update
Set Mask Show Name

Hide A Mask:

Update
Set Mask Hide Name

# Set Mask Color

By default, Masks are displayed in White. You can change this by setting the MaskColor property.

Set MaskColor Color Name

Color must be a .Net Named Color. If you are not sure, use the small box of Crayon colors like Red, Blue, Orange, etc.
Name is the name of the Masque to set the MaskColor. 

Example:
Set MaskColor Turquoise PianoKeys

Note: You must set the MaskColor property prior to showing the Mask.

# Masks Are Being Developed Now
I needed to write this somewhere to code it, and by writing it here first I don't have to come back and write it again.






 



# Normalizing An Image
One of the uses of Transparency Maker is an image that is grainy or pixelated, you can smooth out an area by
setting all pixels in a range to a certain value.

Update<br/>
Set Color 220 0 55<br/>
Where<br/>
Total Between 525 590<br/>
X Between 200 360<br/>
Y > 400<br/>

# Drawing Transparent Lines

To Draw a Transparent Line, Type The Following

Line 1: Draw Line LineThickness<br/>
Line 2: First two points are Line Start Point X Y<br/>
        Second two points are Line End Point X Y<br/>
Line 3 (Optional): Repeat Direction Iterations Move<br/>

Draw Line 4<br/>
161  125  457  66<br/>
Repeat Down 15 1<br/>

# Tip: Line 2 can be filled in for you using the Color Picker

Type Line 1 Draw Line (Thickness), then hit enter to place your cursor on the next line.
Ensuriing the Color Picker is toggled on, Click on your image to establish Point 1:

Draw Line 4<br/>
161  125  

Then click your mouse again for the end point of the line:

Draw Line 4<br/>
161  125  457  66

(Optional) Type line 3 if you want to repeat.

Then click Apply.

Draw Line 4<br/>
161  125  457  66<br/>
Repeat Down 15 1<br/>

# Repeat Directions
Use with Repeat with Down, Up, Left and Right

# Repeat Up Example

Draw Line 2<br/>
0  64  457  66<br/>
Repeat Up 100 2<br/>

The above line will be drawn 100 times, and each time it will move the Y coordinate up (minus) two pixels.

Note: The line will go futher than the bounds of the image, so Repeat 50 would accomplish the same thing.

# Repeat Left Example

Draw Line 1<br/>
225  412  105  430<br/>
Repeat Left 25 2<br/>

The above line will be drawn 25 times and will move the StartPoint.X and EndPoint.X left two pixels each iteration.

# Repeat Right Example

Draw Line 5<br/>
140 300 90 600<br/>
Repeat Right 50 10<br/>

The above line will draw transparent stripes through an image because the Repeat Move (last parameter) is greater than the line thickness from Line 1.

# Draw Transparent Lines Are Slow In Large Images
Drawing transparent lines is slower than any other operation using Transparency Maker because what it actually does is create a copy of the source image and draws a line trhough the copy in a color that doesn't exist in the copy, then creates a Pixel Database out of the copy image and determines which pixels were modified. The pixels that were modified in the copy are then applied to the source Pixel Database and set to Transparent.

I think this is a good candidate for letting people with a powerful GPU take advantage of this, but I haven't learned how to apply this yet. I have a 1080 TI, but I have never written any code to take advantage of a GPU. Volunteers anyone?

# Masks



# Known Issues
Clicking Save sometimes causes an error. You can think click Continue, and are returned to your image.
Click Save As works.

# Upcoming Features
It has been on my list for a long time to add a Save prompt if there are changes and you attempt to close the file or the application.

Let me know what features you think are needed or any feedback you may have.

# Draw Line In Color
I thought I had code to draw a line in a specific color, but this has not been done yet apparently. It will be coming soon.

If anyone wants this feature let me know.

Any suggestions or feedback is welcome.


























