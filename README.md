# streaming-parser

## Overview

The aim of this project is to make it easy to covert sections of large XML documents into serialized .NET objects, without loading everything into memory at once. The expectation is that there are classes already defined for the content of the document. This means that navigation can be specified using your typed class structure, but actual deserialization occurs after navigation.

The idea for this solution came up as a bit of a thought experiment, in response to a problem that came up in a workplace discussion. A large xml file is sitting on disk, and the 'easiest' approach to accessing content is obviously to use a plain old [XmlSerializer.Deserialize()](https://msdn.microsoft.com/en-us/library/system.xml.serialization.xmlserializer.deserialize%28v=vs.110%29.aspx). If we already have the classes defined, is there a way to 'navigate' the hierarchy first, and the deserialize only the chunks that you are interested in?

The solution is effectively using 2 very basic .NET xml tools:

* An XmlReader to perform the initial navigation
* An XmlSerializer to retrieve the desired objects from the xml content

The solution here is aimed at filling in those gaps where a large document is already be loaded in a single hit, and we want to optimise it. If you are really trying to shave off milliseconds and save kilobytes, you probably want some custom deserialization for your specific document format.

This project is also an excuse for me personally to try out publishing a small, self-contained project. Hopefully others may find it fills this niche, and can be used to solve this specific problem.

## Example

```cs
// Grab a stream that contains lots of XML data
var assembly = Assembly.GetExecutingAssembly();
Stream largeXmlDataStream = assembly.GetManifestResourceStream("MyAssembly.largeEmbeddedFile.xml");

// Construct your root parser object. This can be used for multiple navigation and generation requests
var parser = new XmlStreamingParser<RootObject>(largeXmlDataStream);

// Perform a navigation to a nested object. There is still no data loaded into memory. In fact, even the navigation of the underlying reader only occurs at Generate time
IGraphNavigator<TypeOfTier1Node> nav = parser.Navigate(r => r.ObjectAtTier1);

// Let's load some data. The previous navigation requests, plus the final path for generation, are used to move the underlying reader.
// Finally, a standard deserialize takes place to return your object.
TypeOfObjectAtTier2 deserializedResult = nav.Generate(t1 => t1.ObjectAtTier2);

// How about enumerating a list of items?
IEnumerable<TypeOfListItems> itemEnumeration = nav.GenerateElements(t1 => ListOfItemsInTier2)

```

Navigation is not strictly necesary. The object graph path in a Generate request is equivalent to a Navigate. It is useful for partial navigation to an intermediate location, with further additional navigation and generation requests flowing from this point.

## Notes

