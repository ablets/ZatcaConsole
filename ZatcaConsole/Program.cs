using ZatcaInvoiceHash;

do
{
    Console.WriteLine("\nProvide an absolute file path of the xml\n");
    string? xmlFilePath = Console.ReadLine();
    if (string.IsNullOrEmpty(xmlFilePath))
    {
        xmlFilePath = Path.Combine(Environment.CurrentDirectory, "xml/sample.xml");
        Console.WriteLine($"Using example file: {xmlFilePath} ");

    }

    xmlFilePath = xmlFilePath.Replace("\"", "").Replace("'", "").Trim();
    bool isAbsolutePath = Path.IsPathRooted(xmlFilePath); // Returns true

    if (!isAbsolutePath)
    {
        Console.WriteLine("Not an absolute path");
    }

    string fatooraPath = @"";
    string invoiceHash = InvoiceHashService.GenerateInvoiceHash(xmlFilePath, fatooraPath);
    if(string.IsNullOrEmpty(invoiceHash))
    {
        Console.WriteLine("Could not generate invoice hash");
    }
    else
    {
        Console.WriteLine($"Invoice hash: {invoiceHash}");
    }
}
while
(true);


