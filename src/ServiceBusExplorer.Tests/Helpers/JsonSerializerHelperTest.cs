﻿using ServiceBusExplorer.Utilities.Helpers;
using FluentAssertions;
using Xunit;

namespace ServiceBusExplorer.Tests.Helpers
{
    public class JsonSerializerHelperTest
    {
        [Fact]
        public void IsJson_ReturnsFalse_WhenNullInput()
        {
            var result = JsonSerializerHelper.IsJson(null);

            result.Should().BeFalse();
        }

        [Fact]
        public void IndentJson_ValueIsNotJson_ReturnsOriginalString()
        {
            var myOriginalString = "This is a full text string that is not a JSON";
            var indented = JsonSerializerHelper.Indent(myOriginalString);

            indented.Should().Be(myOriginalString);
        }

        [Fact]
        public void IndentJson_ValueIsXml_ReturnsOriginalString()
        {
            var myOriginalString = "<sample><title>XML tile</title><alternate language=\"en\">A tile made from some classical XML content.</language></sample>";
            var indented = JsonSerializerHelper.Indent(myOriginalString);

            indented.Should().Be(myOriginalString);
        }

        [Fact]
        public void IndentJson_ValueIsJson_ReturnsIndentedString()
        {
            var json = "{prop1:\"val1\",prop2:2,\"prop3\":[1, 2, 3],prop4:{subProp1:1,subProp2:\"string\",subProp3:[\"a\",\"b\",\"c\"]}}";
            var expectedResult = @"{
  ""prop1"": ""val1"",
  ""prop2"": 2,
  ""prop3"": [
    1,
    2,
    3
  ],
  ""prop4"": {
    ""subProp1"": 1,
    ""subProp2"": ""string"",
    ""subProp3"": [
      ""a"",
      ""b"",
      ""c""
    ]
  }
}";
            var indented = JsonSerializerHelper.Indent(json);

            indented.Should().Be(expectedResult);
        }
        
        [Fact]
        public void IndentJson_ValueIsJson_DoesNotChangeDateFormat()
        {
            var json = @"{""dateIso"":""2018-05-14T00:00:00Z"",""dateMicrosoft"":""/Date(1526256000000)/""}";
            var expectedResult = @"{
  ""dateIso"": ""2018-05-14T00:00:00Z"",
  ""dateMicrosoft"": ""/Date(1526256000000)/""
}";
            var indented = JsonSerializerHelper.Indent(json);

            expectedResult.Should().Be(indented);
        }

        [Fact]
        public void IndentJson_ValueIsMalformedJson_ReturnsOriginalJson()
        {
            var json = @"{""Field1"":""Value1"",""Field1"":""Value2""}";
            var expectedResult = @"{""Field1"":""Value1"",""Field1"":""Value2""}";
            var indented = JsonSerializerHelper.Indent(json);

            expectedResult.Should().Be(indented);
        }

        [Fact]
        public void IndentJson_ValueHasTypeHandling_ReturnsIndentedStringWithTypeHandling()
        {
            var json = "{\"$type\":\"MyAwesomeLibrary.MyAwesomeClass\",prop1:1,prop2:2,obj:{\"$type\":\"MyAwesomeLibrary.MyOtherAwesomeClass\",default:true}}";
            var expectedResult = @"{
  ""$type"": ""MyAwesomeLibrary.MyAwesomeClass"",
  ""prop1"": 1,
  ""prop2"": 2,
  ""obj"": {
    ""$type"": ""MyAwesomeLibrary.MyOtherAwesomeClass"",
    ""default"": true
  }
}";
            var indented = JsonSerializerHelper.Indent(json);

            indented.Should().Be(expectedResult);
        }
    }
}
