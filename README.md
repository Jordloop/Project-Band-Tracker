# Band Tracker


#### By Jordan Loop; Date of current version: 6/16/2017

## Description

#### This program tracks bands and the venues they have played concerts at.

## Setup/Installation Requirements

1. Clone git repo.
2. Create database:
    In SQLCMD:
    CREATE DATABASE band_tracker; > GO > USE band_tracker > GO > CREATE TABLE venues(id INT IDENTITY(1,1), name VARCHAR(255)); > GO > CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255)); > GO > CREATE TABLE band_venue_join(id INT IDENTITY(1,1), band_id INT, venue_id INT); > GO
3. Start local Server:
    In PowerShell:
      dnx kestrel

## Known Bugs

No know bugs.

## Questions/Concerns or advice?

Contact me at jordanloop@gmail.com

## Technologies Used

* Nancy
* ASP.NET 5
* C#
* HTML
* Xunit

### License

None.

## Specifications
| Behavior handled<br>By this program:            | Input example<br>When it receives:           | Output example<br>It should return:                                          |
|-------------------------------------------------|----------------------------------------------|------------------------------------------------------------------------------|
| User can select to see all bands.               | Clicks "View Bands" button.                  | Displays a list of all bands.                                                |
| User can create a new band.                     | Fill out form and click "Add Band" button.   | Displays a list of all bands.                                                |
| User can select a specific band to see details. | Clicks "band-name" link.                     | Displays band's name.                                                        |
| User can select a specific band to see details. | Clicks "band-name" link.                     | Displays band's name and each venue they have played at.                     |
| User can add a venue to the band.               | Fills out form and click "Add Venue" button. | Displays band's name and each venue they have played at.                     |
| User can select to see all venues.              | Clicks "View Venues" button.                 | Displays a list of all venues.                                               |
| User can create a new venue.                    | Fills out form and click "Add Venue" button. | Displays a list of all venues.                                               |
| User can select a specific band to see details. | Clicks "venue-name" link.                    | Displays venue's name.                                                       |
|  User can add a band to the venue.              | Fills out form and clicks "Add Band" button. | Displays venue's name and each band that has played there.                   |
| User can delete a venue.                        | Click "Delete Venue" button.                 | Displays venue's name and a list of all venues, but deleted venue is absent. |
| User can update a venue's name.                 | Click Update button.                         | Displays a form.                                                             |
| User can update a venue's name.                 | Fill out form and click "Submit" button.     | Displays venue's updated name and each band that has played there.           |
