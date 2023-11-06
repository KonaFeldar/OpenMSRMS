# OpenMSRMS
Open Source Project to build a retail management system on top of an existing Microsoft RMS database. Since Microsoft RMS reached its end of life I would like to build a replacement that still uses the existing data.

Notes:
Need to build a good installer that can optionally create a new database from an RMS template or connect to an existing RMS database.
Out of the box I want to be able to read and modify the existing entries as originally intended before extending them with my own functionality.
Installer should be able to add and modify tables if the program updates require some custom tables or attributes.
Make sure the proper prerequisites are installed and have the ability to include optional components.
So far NPOI has been the easiest to work with for me, but Wix also seemed good for windows projects.

I’d like to make the program modular, a lot of the base functionality will be used in several places and needs to be accessible from each piece:
(Database connectivity, products, customers, purchase orders) I tried to see if I could download a copy of the MS-RMS source code to start with, but
just because it’s EOL doesn’t mean it’s free to modify…

I’d also like to make the program moddable, an addon folder can be included and the addons can be added to the menus for features that aren’t included
in the base program, but still access the base functions… (Interfaces? Prism seemed as good a place to start as any.)
https://learn.microsoft.com/en-us/dotnet/framework/mef/

I did set up a pretty decent main form with QIOS devsuite ribbon and buttons, but I know that solution is a bit outdated and might possibly have a more recent replacement.
I tried WPF, but the form building from XML wasn’t as intuitive as the drag and drop of a windows form and handling the events was quite different as well.
It’s a bit more like a webpage, which is probably great for someone who is good in that area… I’m open to suggestions.
Qios.Devsuite is included in this repository, I do still like the way it works.

The main form is an MDI parent and the child forms should stay within the work area of the main form.
The main form will also have a launch pad as the first child form to open up an easy to use shortcut to the main functions.

Still working out the best place to store the database connection that’s secure and not easy to fish out or tamper with…
I think I settled on the registry with mdi hash code to hide it. This solution might not be cross-platform friendly, but MS-RMS is a windows product anyway.

RMS can handle multiple stores and a headquarters database, but product changes made in a store are local and don't update everywhere (ex: sale prices).
I’d like to, at least optionally, have (some) changes be applied to all active databases and not have to explicitly modify them in the headquarters database
and run a worksheet to each store. This would require connections for each database to be stored and not just the one. And all Databases would need to be the same.
The majority of sync tasks should probably still work about the same as Headquarters does and have the master items and sales in the cloud and sync
the local databases after hours… After the stores sync any inventory modules could be run for the websites off the cloud database.

The backstock program should also be implemented here as an addon. We should be able to see the current inventory for ALL stores and split the inventory by location
(out on the floor, backstocked upstairs, in storage) This is an expanded bin location that keeps separate stock counts for each bin.

I’m not worried about the POS at this time, there’s a whole PCI compliance hurdle and credit card processors to connect to that might make this step overly complicated.
