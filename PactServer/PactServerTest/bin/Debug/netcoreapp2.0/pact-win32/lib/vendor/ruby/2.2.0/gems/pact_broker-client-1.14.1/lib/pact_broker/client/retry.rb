require 'pact_broker/client/error'

module PactBroker
  module Client
    class Retry
      class RescuableError
        UNRESCUEABLE = [PactBroker::Client::Error]

        def self.===(e)
          case e
          when *UNRESCUEABLE then false
          else true
          end
        end
      end

      def self.until_true options = {}
        max_tries = options.fetch(:times, 3)
        tries = 0
        while true
          begin
            return yield
          rescue RescuableError => e
            tries += 1
            $stderr.puts "Error making request - #{e.class} #{e.message} #{e.backtrace.find{|l| l.include?('pact_broker-client')}}, attempt #{tries} of #{max_tries}"
            raise e if max_tries == tries
            sleep options
          end
        end
      end

      def self.sleep options
        Kernel.sleep options.fetch(:sleep, 5)
      end
    end
  end
end
