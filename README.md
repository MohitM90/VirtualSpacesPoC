# VirtualSpacesPoC
A sample app showing a Web App calling a protected Web API that calls Graph API on behalf of the signed in user

#### Configure Known Client Applications for service (TodoListService(ms-identity-dotnet-native-aspnetcore-v2))

For a middle tier Web API (`VirtualSpaces.API`) to be able to call a downstream Web API (`Graph API`), the middle tier app needs to be granted the required permissions as well.
However, since the middle tier cannot interact with the signed-in user, it needs to be explicitly bound to the client app in its Azure AD registration.
This binding merges the permissions required by both the client and the middle tier Web API and presents it to the end user in a single consent dialog. The user then consent to this combined set of permissions.

To achieve this, you need to add the **Application Id** of the client app (`VirtualSpaces.WEB`), in the Manifest of the Web API in the `knownClientApplications` property. Here's how:

1. In the [Azure portal](https://portal.azure.com), navigate to your `VirtualSpaces.API` app registration, and select **Manifest** section.
1. In the manifest editor, change the `"knownClientApplications": []` line so that the array contains 
   the Client ID of the client application (`VirtualSpaces.WEB`) as an element of the array.

    For instance:

    ```json
    "knownClientApplications": ["ca8dca8d-f828-4f08-82f5-325e1a1c6428"],
    ```

1. **Save** the changes to the manifest.
