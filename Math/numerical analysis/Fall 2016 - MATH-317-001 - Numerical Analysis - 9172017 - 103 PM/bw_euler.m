% Newton's law of cooling for a cup of coffee

T0 = 95;               % initial temperature of coffee, Celsius
k = 0.24;              % constant from problem, the constant of proportionality
Tenv = 20;             % room temperature
dT = @(T) -k*(T-Tenv); % derivative of temperaturse, from ODE
I = [0,20];            % time interval to solve the ODE on, in minutes
h = 5e-1;              % time step

t = I(1):h:I(2);       % discrete time

T = zeros(size(t));    % pre allocate room for the solution
T(1) = T0;             % set the initial temperature
i = 1;                 % index for problem

% now actually solve the problem
while i < length(t);
  % we need an initial guess for the root solver, use forward Euler
  Tguess = T(i) + h*dT(T(i));

  % Use backward Euler to solve for next time step.
  % This means we need to solve an implicit equationing a root solver.
  % Here I use newton's method, but you could use fzero instead
  T(i+1) = newton(Tguess, ...
                    @(x) T(i)+h*dT(x) - x,...
                    @(x) -k*h-1,...
                    1000,1e-3 );

  %% Example with fzero instead
  %T(i+1) = fzero(@(x) T(i)+h*dT(x) - x,...
  %                 Tguess);
  
  i = i+1; % increment time step
end

Ttrue = (T0-Tenv)*exp(-k*t)+Tenv;

plot(t,T,'.',t,Ttrue);
xlabel('$t$ (minutes)', 'interpreter','latex');
ylabel('$T$ (Celsius)', 'interpreter','latex');
leg = legend('Computed solution','True solution');
set(leg,'interpreter','latex')


err = max(abs(Ttrue-T));
