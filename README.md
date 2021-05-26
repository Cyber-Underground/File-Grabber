# File-grabber
File grabber which sends specified files to FTP server

Simple C# script to steal files from desktop, easy to modify for other usage.

You also may consider changing your FTP account permissions to write only to prevent hackers getting into your FTP server and installing your files as credientals are hardcoded!
Here's how to do that:
```
First do sudo vim /etc/vsftpd.conf or sudo nano /etc/vsftpd.conf

    In the settings file look for the line write_enable=YES which will be probably commented out #write_enable=YES, uncomment it removing the # from the front and save the file.
    Then search for 
    Finally restart the vsftpd service using:

sudo service vsftpd restart
```
