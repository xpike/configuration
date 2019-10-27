# xPike.Configuration.AWS

Provides configuration values from AWS SSM Simple Parameter Service.

Uses attached IAM role/policy to acquire access to SSM.

Supports decryption of values encrypted using KMS.

## Exported Services

### Singleton Registrations

- **`IAWSConfigurationProvider`**  
  **=> `AWSConfigurationProvider`**

### Collection Registrations

- **`IConfigurationProvider`**  
  **Add: `IAWSConfigurationProvider`**

## Dependencies

- XPike.Configuration

