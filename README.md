# WinRT-StandardDateFormatPicker
Simple control based on ComboBoxes to pick date in common in Europe and most of the world format dd-mm-yyyy.

Wiki: https://github.com/ignacy130/WinRT-StandardDateFormatPicker/wiki

Usage:

```
<dp:DatePicker x:Name="DatePicker" MinYear="2011" MaxYear="2100" Month="9" MinMonth="1" MaxMonth="8"  Year="200" />
<TextBlock DataContext="{Binding Date, ElementName=DatePicker}" >
    <Run Text="{Binding Day}"/> /
    <Run Text="{Binding Month}"/> /
    <Run Text="{Binding Year}"/>
</TextBlock>
```

Any help, solutions and commits are highly appreciated! ;)
