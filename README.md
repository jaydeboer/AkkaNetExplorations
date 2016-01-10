# AkkaNetExplorations
This is just a place to share my first wanderings into AKKA.net

## Sample1
Yup you guessed it, my first sample project.

## Sample3
Expanding on Sample 1 above by adding some services that our actors can, well, act upon.

#### Build instructions

From the Sample3/ folder, you can run your

```
> dnu restore
```

for restoring dependencies and to actually build the full suite,

```
> dnu build ./**
```

For testing, you will need to drop down into the specific testing projects and then run 

```
> dnx test
```

This will kick off the xUnit tests in that project.

