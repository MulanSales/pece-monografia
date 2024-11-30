using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using Stardust.Manager.Extensions;
using Stardust.Manager.Helpers;
using Stardust.Manager.Interfaces;
using Stardust.Manager.Models;

namespace Stardust.Manager
{
public class JobRepository : IJobRepository
{
private readonly RetryPolicyProvider _retryPolicyProvider;
private readonly RetryPolicy _retryPolicy;
private readonly string _connectionString;

public JobRepository(string connectionString, RetryPolicyProvider retryPolicyProvider)
{
_connectionString = connectionString;
_retryPolicyProvider = retryPolicyProvider;
_retryPolicy = retryPolicyProvider.GetPolicy();
}
}
}