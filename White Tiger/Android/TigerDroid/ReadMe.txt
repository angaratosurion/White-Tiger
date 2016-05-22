Easy WSDL

http://easywsdl.com


How to use generated classes:

1. In your android project create a new package folder (name of this folder should match the Package name of generated classes)
2. Go to src subfolder of generated package and copy all files to your project (to newly created folder).
3. In libs subfolder you will find all jars needed for generated classes. Copy all jars to your android project and add references to them (every IDE has different way of adding refenreces to external jars)    
4. Use the following code to connect to your webservice:

PCLservice service = new PCLservice();
service.MethodToInvoke(...);


Used libraries:

- ksoap2 v3.3.0 (https://code.google.com/p/ksoap2-android/)  


Thanks for using EasyWsdl. We've spend much time to create this tool. We hope that it will simplify your development. If you like it, please upvote posts about it on stackoverflow and like us on Facebook (https://www.facebook.com/EasyWsdl).
This will help us promote the generator. If you find any problems then contact us and we will try to help you with your webservice.


Generator output:
1SoapClientGenerator.Common.OutputItem2SoapClientGenerator.Common.OutputItem3SoapClientGenerator.Common.OutputItem4SoapClientGenerator.Common.OutputItem