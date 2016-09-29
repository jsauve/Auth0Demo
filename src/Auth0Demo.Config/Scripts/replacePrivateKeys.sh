#!/bin/bash

# move the PrivateKeys.cs file to where the project expects it to be
mv Auth0Demo.Config/Temp/PrivateKeys.cs Auth0Demo.Config/

# replace placeholders with values from CI process' secret vars store
sed -i '' "s/YOUR_AUTH0_DOMAIN/$AUTH0_DOMAIN/g" "Auth0Demo.Config/PrivateKeys.cs"
sed -i '' "s/YOUR_AUTH0_CLIENT_ID/$AUTH0_CLIENT_ID/g" "Auth0Demo.Config/PrivateKeys.cs"