# parity-blitz
Parity Blitz is a game designed to teach how computer parity bits are used to determine if data is corrupt.

## Parity Bits
Computers send data back and forth across the world. Sometimes this data gets corrupted. How does a computer tell if data is good or bad? How does a computer tell if data is corrupted? One method is a parity bit. A parity bit is a 0 or 1 added to the end of a binary number. For example, using even parity, a 0 or 1 is added to the end of a binary number so that ALL of the digits add up to an EVEN number.
- 0001 becomes 00011
- 0011 becomes 00110

When a computer receives data, it checks each binary number. If the digits add up to an even number, the data is likely still good. If the digits add up to an odd number, the data is bad.

00011 is GOOD because...
0+0+1+1=2 which is EVEN

0010 is BAD because...
0+0+1+0=1 which is ODD

## Gameplay
Data is being transmitted using binary numbers and parity bits. A binary number is shown to the player, and a parity bit has been added at the end. The sum of the binary digits is an even number if the data is GOOD and an odd number if the data is BAD. The player must act fast and analyze the digits to decide if the data is good or bad.

![Parity Blitz gameplay](https://github.com/mklewandowski/parity-blitz/blob/main/parity-blitz-gameplay.gif?raw=true)

## Supported Platforms
Parity Blitz is designed for use on the Web.

## Running Locally
Use the following steps to run locally:
1. Clone this repo
2. Open repo folder using Unity 2021.3.35f1
3. Install Text Mesh Pro

## Building the Project

### WebGL Build
For embedding within itch.io and other web pages, we use the `better-minimal-webgl-template` seen here:
https://seansleblanc.itch.io/better-minimal-webgl-template

Setup of the `better-minimal-webgl-template` is as follows:
1. Download and unzip the template.
2. Copy the `WebGLTemplates` folder into the `Assets` folder.
3. File -> Build Settings... -> WebGL -> Player Settings... -> Select the "BetterMinimal" template.
4. Enter color in the "Background" field.
5. Enter "false" in the "Scale to fit" field to disable scaling.
6. Enter "true" in the "Optimize for pixel art" field to use CSS more appropriate for pixel art.

### Running a Unity WebGL Build
1. Install the "Live Server" VS Code extension.
2. Open the WebGL build output directory with VS Code.
3. Right-click `index.html`, and select "Open with Live Server".

## Development Tools
- Created using Unity
- Code edited using Visual Studio Code

