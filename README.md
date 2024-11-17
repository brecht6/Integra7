# Integra-7 Aural Alchemist

A cross-platform editor (only tested on linux so far) for the Roland Integra-7 parameters.

Current functionality
 - At the moment all documented parameters are exposed and you can load SRX/EnSNx from the UI.
 - If you edit something on the integra-7, the tool updates its ui to show the changes
 - Vice versa, if you edit a parameter in the tool, it is sent to the integra-7
 - After editing a tone you can save it on the integra-7 as a user preset

# How to build and run from source code

- First install jetbrains rider from https://www.jetbrains.com/rider/ It's free for non-commercial use nowadays.
- In rider, open the solution file Integra7AuralAlchemist.sln
- In the *run* menu of rider, click run. The tool should build in a few seconds and then start.

# Some screenshots:

Example of how parameters are exposed for a SuperNATURAL Drum kit (the other types of instruments are supported too)
<img src="https://github.com/shimpe/Integra7AuralAlchemist/blob/main/Screenshot/Parameters.png?raw=true" width="800"/>

Example of the SRX selection screen
<img src="https://github.com/shimpe/Integra7AuralAlchemist/blob/main/Screenshot/SrxLoader.png?raw=true" width="800"/>

Example of how you can filter presets and parameters in the ui
<img src="https://github.com/shimpe/Integra7AuralAlchemist/blob/main/Screenshot/Filtering.png?raw=true" width="800"/>
