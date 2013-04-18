xmltransform
============
Simple application which allows to transform one XML file into another using XDT transformations (http://msdn.microsoft.com/en-us/library/dd465326.aspx).


# Usage:
Invocation like this:

`
xmltransform -i input.xml -t .transform.xml -o out.xml name=my-mail
`

will transform file `input.xml` into file `out.xml` using transformation from file `transform.xml`. During transformation all occurences of `{name}` in `transform.xml` will be replaced by `my-mail`.

# Download
Utility could be added to solution using NuGet (https://nuget.org/packages/XmlTransform/). After installing package, `XmlTransform.exe` will be in `[SolutionRoot]\packages\XmlTransform.1.0.0.0\tools\XmlTransform.exe`
Standalone package can also be download here: https://www.dropbox.com/s/uxyc0iqj9msswuc/XmlTransform-1.0.0.0.zip
