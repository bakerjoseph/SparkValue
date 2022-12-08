<p align="center">
  <img src="https://user-images.githubusercontent.com/79991950/206536577-c8fde399-9803-4435-9394-aa0d97a354bb.png" width="150" />
</p>

# Spark Value
Welcome to Spark Value, a desktop application designed for learning basic electronics. There are five units that cover everything from electricity and diodes to printed circuit boards and multimeters. Each lesson contains either interactive content or pictures to further aid in explaining the text content. You can also enjoy an interactive virtual breadboard that allows you to build simple circuits and view component values.

## Get Started
Currently there is no download page for the application, but you can download this repository and run the .exe file found at: ../SparkValueDesktopApplication/bin/Debug/net6.0-windows/SparkValueDesktopApplication.exe

However, you will need MongoDB installed for it to work properly. You can download it [here](https://www.mongodb.com/docs/manual/installation/) and follow their directions to get it setup. 

Optionally, you can use a Twilio SendGrid API key to enable the emailing service. If you have a key, in Microsoft Visual Studio right click the SparkValueBackend project and select **Manage User Secrets**. It may ask to install some packages, just say yes and accept them. Follow the below format to input your key. Along as it is valid, everything should work and you will get welcome and password reset emails accordingly.

~~~
{
  "Email": {
      "ApiKey": "Your key here",
      "SendingEmail": "Email source here"
  }
}
~~~

## Support
If any problems are encountered, please create an issue and let it be known.
