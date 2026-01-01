//@IntentMerge()

// interface for all environment configuration
//@IntentMerge()
export interface AppEnvironment {
  intentSampleAngularBackendServiceConfig: IntentSampleAngularBackendServiceConfig;
}

// base config for http service proxies
export interface HttpConfig {
  baseUrl: string;
  retries?: number;
  timeoutMs?: number;
}

// interface allows for overrides per service
export interface ServiceOverride {
  baseUrl?: string;
  retries?: number;
  timeoutMs?: number;
}

// optional overrides for IntentSampleAngularBackendService
export interface IntentSampleAngularBackendServiceServicesConfig {
  brandsService?: ServiceOverride;
  customersService?: ServiceOverride;
  discountsService?: ServiceOverride;
  ordersService?: ServiceOverride;
  productsService?: ServiceOverride;
  titlesService?: ServiceOverride;
}

// specific configuration for IntentSampleAngularBackendService
export interface IntentSampleAngularBackendServiceConfig extends HttpConfig {
  services?: IntentSampleAngularBackendServiceServicesConfig;
}
